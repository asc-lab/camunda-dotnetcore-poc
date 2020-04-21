import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TaskListComponent } from './task-list/task-list.component';
import { NewOrderComponent } from './new-order/new-order.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_services/auth-guard';


const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home' , component: TaskListComponent, canActivate: [AuthGuard]},
  { path: 'tasks' , component: TaskListComponent, canActivate: [AuthGuard]},
  { path: 'new-order', component: NewOrderComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
