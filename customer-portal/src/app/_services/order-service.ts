import { Injectable } from '@angular/core';
import { PlaceOrderDto, OrderDto } from '../_model/order.dto';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

@Injectable()
export class OrderService {
    constructor(private http: HttpClient) {}
    
    placeOrder(order: PlaceOrderDto): Promise<any> {
        return this.http.post<any>(`${environment.apiUrl}/Order`, order).toPromise();
    }

    getCustomerOrder(status: String): Promise<OrderDto[]> {
        return this.http.get<OrderDto[]>(`${environment.apiUrl}/Client/Orders`).toPromise();
    }
}