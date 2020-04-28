import { Injectable } from '@angular/core';
import { PlaceOrderDto, OrderDto } from '../_model/order.dto';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class OrderService {
    constructor(private http: HttpClient) {}
    
    placeOrder(order: PlaceOrderDto): Promise<any> {
        return this.http.post<any>('http://localhost:6080/Order', order).toPromise();
    }

    getCustomerOrder(status: String): Promise<OrderDto[]> {
        return this.http.get<OrderDto[]>('http://localhost:6080/Client/Orders').toPromise();
    }
}