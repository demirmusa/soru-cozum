import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {AuthGuard} from "./_helpers/auth.guard";
import {LoginComponent} from "./login/login.component";

const routes: Routes = [
  {path: '', component: HomeComponent, canActivate: [AuthGuard]},//anasayfaya login olmayan giremez o nedenle guard tanımlandı
  {path: 'login', component: LoginComponent},

  //geri kalan tüm urlleri anasayfaya yolla
  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
