import {Injectable} from '@angular/core';
import {User} from "../_models/user";
import {BehaviorSubject, Observable,} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";

@Injectable({providedIn: 'root'})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, {username, password})
      .pipe(map(user => {
        //Kullanıcı login oldu. Sayfa refresh olduğunda yada başka sekmelere gidildiğinde veriye erişebilmek için veriyi local storage da tut.
        //Aynı zamanda bellekte bir BehaviorSubject içerisinde tut
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  logout() {
    //kullanıcı bilgisini localstoragedan sil
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
