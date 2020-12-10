import {Injectable} from '@angular/core';
import {HttpRequest, HttpHandler, HttpEvent, HttpInterceptor} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AuthenticationService} from "../_services/authentication.service";


/*
 *Her requestten önce kullanıcının bearer tokenını requeste ekleyen interceptor
 */
@Injectable()
export class JwtAuthInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //kullanıcıyı al
    let currentUser = this.authenticationService.currentUserValue;
    if (currentUser && currentUser.token) {
      //kullanıcı varsa requesti Authorization headera sahip bir klonuyla değiştir
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      });
    }

    return next.handle(request);
  }
}
