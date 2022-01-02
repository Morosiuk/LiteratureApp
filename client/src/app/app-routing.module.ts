import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

import { AuthGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

import { LiteratureItemComponent } from './literature/literature-item/literature-item.component';
import { LiteratureComponent } from './literature/literature/literature.component';
import { LiteratureAddComponent } from './literature/literature-add/literature-add.component';
import { RequestItemComponent } from './requests/request-item/request-item.component';
import { RequestComponent } from './requests/request/request.component';
import { CongregationItemComponent } from './congregations/congregation-item/congregation-item.component';
import { CongregationComponent } from './congregations/congregation/congregation.component';
import { DeliveryComponent } from './deliveries/delivery/delivery.component';
import { DeliveryItemComponent } from './deliveries/delivery-item/delivery-item.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'literature', component: LiteratureComponent},
      {path: 'literature/add', component: LiteratureAddComponent},
      {path: 'literature/:id', component: LiteratureItemComponent},
      {path: 'request', component: RequestComponent},
      {path: 'request/:id', component: RequestItemComponent},
      {path: 'congregation', component: CongregationComponent},
      {path: 'congregation/:id', component: CongregationItemComponent},
      {path: 'delivery', component: DeliveryComponent},
      {path: 'delivery/:id', component: DeliveryItemComponent}
    ]
  },
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
