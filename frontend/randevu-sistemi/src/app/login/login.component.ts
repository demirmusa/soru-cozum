import {Component, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import {AuthenticationService} from "../_services/authentication.service";

@Component({
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {
  loading = false;
  returnUrl: string;

  username: string;
  password: string;

  error: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    //Zaten giriş yapılmışsa direkt anasayfaya yolla
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    //Bir return url ile gelmişse onu al, login olduktan sonra returnurl ye geri yolla
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  onSubmit() {
    this.loading = true;
    this.authenticationService.login(this.username, this.password)
      .subscribe(data => {
          //login oldu
          this.router.navigate([this.returnUrl]);
        },
        error => {//bir hata ile karşılaştık
          this.error = error;
          this.loading = false;
        });
  }
}
