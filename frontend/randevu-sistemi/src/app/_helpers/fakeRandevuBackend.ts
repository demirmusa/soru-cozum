import {Injectable} from '@angular/core';
import {Randevu} from "../_models/randevu";

/*
* Randevuların tutulduğu backend servisini mocklayan sınıf.
* Randevuları localstorade tutup oradan işlem yapıyor.
* */
@Injectable({
  providedIn: 'root'
})
export class FakeRandevuBackend {
  randevulariListele(userId: number): Randevu[] {
    let randevular = <Randevu[]>JSON.parse(localStorage.getItem(this.getLocalStorageKey(userId)));
    randevular = randevular || [];
    return randevular;
  }

  randevuEkle(randevu: Randevu): void {
    let randevular = this.randevulariListele(randevu.userId);
    randevu.id = Math.floor(Math.random() * 100000);//yeni randevu eklendiğinde rastgele bir id ata
    randevular.push(randevu);
    this.kaydet(randevu.userId, randevular);
  }

  randevuSil(userId: number, randevuId: number): void {
    let randevular = this.randevulariListele(userId);

    let kayit = randevular.filter(r => r.id === randevuId)[0];
    if (!kayit) {
      return;
    }

    randevular = randevular.filter(r => r.id !== randevuId);
    this.kaydet(userId, randevular);
  }

  randevuDuzenle(randevu: Randevu): void {
    let randevular = this.randevulariListele(randevu.userId);

    let kayit = randevular.filter(r => r.id === randevu.id)[0];
    if (!kayit) {
      return;
    }

    randevular = randevular.filter(r => r.id !== randevu.id);
    randevular.push(randevu);
    this.kaydet(randevu.userId, randevular);
  }

  private kaydet(userId: number, randevular: Randevu[]): void {
    localStorage.setItem(this.getLocalStorageKey(userId), JSON.stringify(randevular));
  }

  private getLocalStorageKey(userId: number): string {
    return "Randevular." + userId;
  }
}
