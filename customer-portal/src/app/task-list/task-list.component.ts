import { Component, OnInit } from '@angular/core';
import { OrderService } from '../_services/order-service';
import { OrderDto } from '../_model/order.dto';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  pendingOrders: OrderDto[];

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadOrders();
  }
  
  async loadOrders() {
    this.pendingOrders = await this.orderService.getCustomerOrder('New');
  }

}
