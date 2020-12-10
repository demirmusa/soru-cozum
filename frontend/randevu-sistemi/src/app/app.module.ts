import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {LoginComponent} from './login/login.component';
import {HomeComponent} from './home/home.component';
import {FormsModule} from '@angular/forms';
import {ReactiveFormsModule} from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {JwtAuthInterceptor} from "./_helpers/jwtAuth.interceptor";
import {FakeBackendInterceptor} from "./_helpers/fakeBackend.interceptor";
import {ErrorInterceptor} from "./_helpers/error.interceptor";
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgbModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtAuthInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},

    {provide: HTTP_INTERCEPTORS, useClass: FakeBackendInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
