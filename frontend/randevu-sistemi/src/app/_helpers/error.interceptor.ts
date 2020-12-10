import {Injectable} from '@angular/core';
import {HttpRequest, HttpHandler, HttpEvent, HttpInterceptor} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {AuthenticationService} from "../_services/authentication.service";


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      if (err.status === 401) {
        //401 dönerse kullanıcıyı unauthorized hatası almış demektir. Bizim sistemde yetki kontrolü yok,
        //login olan herkesin yetkisi var olduğu düşünüldüğünde burada hata alıyorsak demekki login olan kişi ile ilgili bir değişiklik var o nedenle logout olsun
        this.authenticationService.logout();
        location.reload(true);
      }

      //eğer apilerimizden hata olduğunda bir user friendly exception fırlatılıyorsa onu da burada yakalayıp direkt ekrana basabiliriz.
      //böylece tüm çağrılar için user friendly exception hatalarını da yakalayıp ekrana basmış oluruz.

      const error = err.error.message || err.statusText;
      return throwError(error);
    }))
  }
}
