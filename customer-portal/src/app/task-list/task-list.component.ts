import { Component, OnInit } from '@angular/core';
import { OrderService } from '../_services/order-service';
import { OrderDto } from '../_model/order.dto';
import { TaskDto } from '../_model/task.dto';
import { TaskService } from '../_services/task-service';
import { AuthService } from '../_services/auth-service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  pendingOrders: OrderDto[];
  myTasks: TaskDto[];

  constructor(
    private orderService: OrderService,
    private taskService: TaskService,
    private auth: AuthService) { }

  ngOnInit(): void {
    this.loadOrders();
    this.loadTasks();
  }
  
  async loadOrders() {
    this.pendingOrders = await this.orderService.getCustomerOrder('New');
  }

  async loadTasks() {
    this.myTasks = await this.taskService.getCustomerTasks();
  }

  performTask(task: TaskDto) {
    //navigate to task screen based on type
  }

  claimTask(task: TaskDto) {
  }

  isAssigneeCurrentUser(task: TaskDto) {
    return task.assignee && task.assignee==this.auth.currentUserValue.user;
  }

}
