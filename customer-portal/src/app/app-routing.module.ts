import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TaskListComponent } from './task-list/task-list.component';
import { NewOrderComponent } from './new-order/new-order.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_services/auth-guard';
import { OfferDetailsComponent } from './offer-details/offer-details.component';


const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home' , component: TaskListComponent, canActivate: [AuthGuard]},
  { path: 'tasks' , component: TaskListComponent, canActivate: [AuthGuard]},
  { path: 'new-order', component: NewOrderComponent, canActivate: [AuthGuard] },
  { path: 'accept-offer/:orderId/:taskId', component: OfferDetailsComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
