import {Injectable,} from '@angular/core';
import {
  HttpRequest,
  HttpResponse,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import {Observable, of, throwError} from 'rxjs';
import {delay, mergeMap, materialize, dematerialize} from 'rxjs/operators';
import {User} from "../_models/user";
import {Randevu} from "../_models/randevu";
import {FakeRandevuBackend} from "./fakeRandevuBackend";

const mockUsers: User[] = [
  {
    id: 1,
    username: "admin",
    password: "123qwe",
    firstName: "Admin",
    lastName: "lastname1"
  },
  {
    id: 2,
    username: "user1",
    password: "123qwe",
    firstName: "User 1",
    lastName: "lastname2"
  },
  {
    id: 3,
    username: "user2",
    password: "123qwe",
    firstName: "User 2",
    lastName: "lastname3"
  },
];

@Injectable()
export class FakeBackendInterceptor implements HttpInterceptor {
  constructor(private _fakeRandevuBackend: FakeRandevuBackend) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const {url, method, headers, body, params} = request;
    var backend = this._fakeRandevuBackend;
    // wrap in delayed observable to simulate server api call
    return of(null)
      .pipe(mergeMap(handleRoute))
      .pipe(materialize()) // call materialize and dematerialize to ensure delay even if an error is thrown (https://github.com/Reactive-Extensions/RxJS/issues/648)
      .pipe(delay(300))
      .pipe(dematerialize());

    //eğer gelen requestler bizim serverlara ait requestler ise onları mocklayıp yerine fake server istekleri gönder
    function handleRoute() {
      switch (true) {
        case url.endsWith('/users/authenticate') && method === 'POST':
          return authenticate();
        case url.indexOf('/randevu') != -1:
          return handleFakeRandevuBackend();
        default:
          // herhangi başka bir requestte devam et
          return next.handle(request);
      }
    }

    // login olurken kullanılan servis
    function authenticate() {
      const {username, password} = body;
      const user = mockUsers.find(x => x.username === username && x.password === password);
      if (!user)
        return error('Username or password is incorrect');

      return ok({
        id: user.id,
        username: user.username,
        firstName: user.firstName,
        lastName: user.lastName,
        token: 'fake-jwt-token'
      })
    }

    //randevu sayfasında kullanılan servis
    function handleFakeRandevuBackend(): Observable<HttpEvent<any>> {
      if (!isLoggedIn())
        return unauthorized();

      switch (method) {
        case "POST": {
          const randevu: Randevu = body;
          backend.randevuEkle(randevu);
          return ok()
        }
        case "PUT": {
          const randevu: Randevu = body;
          backend.randevuDuzenle(randevu);
          return ok()
        }
        case "DELETE": {
          const userId = parseInt(params.get("userId"));
          const randevuId = parseInt(params.get("randevuId"));
          backend.randevuSil(userId, randevuId);
          return ok()
        }
        default: {
          const userId = parseInt(params.get("userId"));
          let randevular = backend.randevulariListele(userId);
          return ok(randevular);
        }
      }
    }

    // helper functions

    function ok(body?) {
      return of(new HttpResponse({status: 200, body}))
    }

    function error(message) {
      return throwError({error: {message}});
    }

    function unauthorized() {
      return throwError({status: 401, error: {message: 'Unauthorised'}});
    }

    function isLoggedIn() {
      return headers.get('Authorization') === 'Bearer fake-jwt-token';
    }
  }
}
