import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';

import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LiteratureComponent } from './literature/literature/literature.component';
import { LiteratureItemComponent } from './literature/literature-item/literature-item.component';
import { RequestItemComponent } from './requests/request-item/request-item.component';
import { CongregationItemComponent } from './congregations/congregation-item/congregation-item.component';
import { DeliveryComponent } from './deliveries/delivery/delivery.component';
import { DeliveryItemComponent } from './deliveries/delivery-item/delivery-item.component';
import { CongregationComponent } from './congregations/congregation/congregation.component';
import { RequestComponent } from './requests/request/request.component';
import { SharedModule } from './_modules/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    LiteratureComponent,
    LiteratureItemComponent,
    RequestItemComponent,
    CongregationItemComponent,
    DeliveryComponent,
    DeliveryItemComponent,
    CongregationComponent,
    RequestComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
