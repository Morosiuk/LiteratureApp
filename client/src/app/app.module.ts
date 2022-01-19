import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LiteratureComponent } from './literature/literature/literature.component';
import { LiteratureItemComponent } from './literature/literature-item/literature-item.component';
import { RequestItemComponent } from './requests/request-item/request-item.component';
import { CongregationListComponent } from './congregations/congregation-list/congregation-list.component';
import { CongregationItemComponent } from './congregations/congregation-item/congregation-item.component';
import { DeliveryComponent } from './deliveries/delivery/delivery.component';
import { DeliveryItemComponent } from './deliveries/delivery-item/delivery-item.component';
import { RequestComponent } from './requests/request/request.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { LiteratureAddComponent } from './literature/literature-add/literature-add.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { UserCardComponent } from './users/user-card/user-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { YesNoPipe } from './shared/yes-no.pipe';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { CongregationCardComponent } from './congregations/congregation-card/congregation-card.component';
import { CongregationAddComponent } from './congregations/congregation-add/congregation-add.component';
import { PublisherCreateComponent } from './users/publisher-create/publisher-create.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    LiteratureComponent,
    LiteratureItemComponent,
    RequestItemComponent,
    CongregationListComponent,
    CongregationItemComponent,
    DeliveryComponent,
    DeliveryItemComponent,
    RequestComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    LiteratureAddComponent,
    UserListComponent,
    UserCardComponent,
    UserEditComponent,
    YesNoPipe,
    TextInputComponent,
    CongregationListComponent,
    CongregationCardComponent,
    CongregationAddComponent,
    PublisherCreateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
