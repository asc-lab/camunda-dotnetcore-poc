import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TaskListComponent } from './task-list/task-list.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_services/auth-guard';
import { CreateOfferComponent } from './create-offer/create-offer.component';
import { InvoicesListComponent } from './invoices-list/invoices-list.component';


const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home' , component: TaskListComponent, canActivate: [AuthGuard]},
  { path: 'tasks' , component: TaskListComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'create-offer/:orderId/:taskId', component: CreateOfferComponent, canActivate: [AuthGuard]},
  { path: 'invoices', component: InvoicesListComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
