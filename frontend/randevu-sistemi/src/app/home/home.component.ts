import {Component, OnInit} from '@angular/core';
import {RandevuService} from "../_services/randevu.service";
import {Randevu} from "../_models/randevu";
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';

/*
* Anasayfa.
* Randevuların yönetildiği component. Login olduktan sonra buraya geliyor.
* */
@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  createOrEditRandevu: Randevu = new Randevu();
  randevular: Randevu[];

  constructor(
    private _randevuService: RandevuService,
    private modalService: NgbModal
  ) {
  }

  ngOnInit(): void {
    this.listele();//sayfa açıldığında verileri listele
  }

  listele() {
    this._randevuService.randevulariListele()
      .subscribe(data => {
        this.randevular = data;
      })
  }

  //ekle/düzenle modalını aç
  open(content: any, randevu?: Randevu) {
    if (randevu) {
      this.createOrEditRandevu = randevu;
    } else {
      this.createOrEditRandevu = new Randevu();
    }

    this.modalService.open(content).result
      .then(
        () => {//modal save e basılarak kapatılmış
          this.kaydet();
        },
        e => {
          //modal save dışında bir şekilde kapatılmışsa işlem yapma
        }
      );
  }

  kaydet() {
    if (this.createOrEditRandevu.id) {
      this._randevuService.randevuDuzenle(this.createOrEditRandevu).subscribe(() => {
        this.listele();
      });
    } else {
      this._randevuService.randevuEkle(this.createOrEditRandevu).subscribe(() => {
        this.listele();
      });
    }
  }

  sil(randevu: Randevu) {
    this._randevuService.randevuSil(randevu.id).subscribe(() => {
      this.listele();
    });
  }
}
