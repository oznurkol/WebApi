﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProje.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        //işlemlerin sırası ile yürütülmeini sağlar
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            
            //eğer autHeader bo ise diğerişleme geç ve return t boşuna authentication yapma
            if(authHeader == null)
            {
                //context.Response.StatusCode = 401;
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //await _next(context);
                //return;
            }

            //web apilerde hang, tür ile authentice edildiğini aşağıdaki şekilde tanımlar
            //basic (authentication türü) engin(kullanıcı adı):12345 (şifre) => basic engin:12345
            //or token (authentication türü) engin(kullanıcı adı):12345 (şifre)
            //yukarıdaki gibi data gönderilir
                
            //basic authentcation formatı => basic engin:12345
            else if (authHeader != null && authHeader.StartsWith("basic",StringComparison.OrdinalIgnoreCase)) //autHeader da authentica değeri var mı ve basic le başlıyor mu
            {
                var token = authHeader.Substring(6).Trim(); //kullanıcı adı ve şifre kısmını aldımüstteki formattan
                var credentialString = "";
                try
                {
                    //kullanıcı adı ve şifre yani data Base64 olarak gönderlmli
                    credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));//token da ki bilgi normal string e dönüştürüldü
                }
                catch
                {
                    context.Response.StatusCode = 500;
                }
                //elimde engin:12345 var
                var credentials = credentialString.Split(':');
                if(credentials[0]=="engin" && credentials[1] == "12345") //db kontrol olacak
                {
                    //bir principle tanımlamamız gerekiyor. princible bir identitydir
                    //authorize da rol verip adminler bu işi yapabilir dediğimzde
                    //bu bir principle olur bir claim olusturucaz
                    var claims = new[]
                    {
                        new Claim("name",credentials[0]),
                        new Claim(ClaimTypes.Role,"Admin")//value admin kısmı db den gelebilir
                    };
                    var identity = new ClaimsIdentity(claims,"Basic"); //bir identity oluturuldu
                    //mevcut context in yani db nin user tablosu
                    context.User = new ClaimsPrincipal(identity); //kişisi şu olacak
                }
            }
            else
            {
                //autHeader yoksa bir data girilmediyse 401 gönderilir
                context.Response.StatusCode = 401; //authentice değil
            }

            await _next(context); //sonraki iş katmanına bu context teki iş ak
        }
    }
}
