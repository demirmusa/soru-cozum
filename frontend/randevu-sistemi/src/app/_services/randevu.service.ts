import {Injectable} from '@angular/core';
import {Randevu} from "../_models/randevu";
import {AuthenticationService} from "./authentication.service";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";

/*
* HttpClient ile servera istekte bulunup işlem yapan sınıf
* */
@Injectable({
  providedIn: 'root'
})
export class RandevuService {
  constructor(
    private _authService: AuthenticationService,
    private http: HttpClient
  ) {
  }

  private static get apiUrl() {
    //ana urlyi alırken environmenttan al.
    return `${environment.apiUrl}/randevu`;
  }

  randevulariListele(): Observable<Randevu[]> {
    let user = this._authService.currentUserValue;
    return this.http.get<Randevu[]>(RandevuService.apiUrl, {params: {userId: user.id.toString()}});
  }

  randevuEkle(randevu: Randevu): Observable<any> {
    let user = this._authService.currentUserValue;
    randevu.userId = user.id;
    return this.http.post(RandevuService.apiUrl, randevu);
  }

  randevuSil(randevuId: number): Observable<any> {
    let user = this._authService.currentUserValue;
    return this.http.delete(RandevuService.apiUrl, {
      params: {
        userId: user.id.toString(),
        randevuId: randevuId.toString()
      }
    });
  }

  randevuDuzenle(randevu: Randevu): Observable<any> {
    let user = this._authService.currentUserValue;
    randevu.userId = user.id;
    return this.http.put(RandevuService.apiUrl, randevu);
  }
}
