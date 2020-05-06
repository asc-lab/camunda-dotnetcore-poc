import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ClarityModule } from "@clr/angular";
import { TaskListComponent } from './task-list/task-list.component';

import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AuthService } from './_services/auth-service';
import { JwtInterceptor } from './_services/jwt-interceptor';
import { TaskService } from './_services/task-service';
import { CreateOfferComponent } from './create-offer/create-offer.component';
import { SuperPowerService } from './_services/superpower-service';
import { HeroService } from './_services/hero-service';
import { OrderService } from './_services/order-service';

@NgModule({
  declarations: [
    AppComponent,
    TaskListComponent,
    LoginComponent,
    CreateOfferComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ClarityModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    
    AuthService,
    TaskService,
    SuperPowerService,
    HeroService,
    OrderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
