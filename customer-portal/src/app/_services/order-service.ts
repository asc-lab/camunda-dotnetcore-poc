import { Injectable } from '@angular/core';
import { PlaceOrderDto, OrderDto, AcceptOfferDto, RejectOfferDto } from '../_model/order.dto';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

@Injectable()
export class OrderService {
    constructor(private http: HttpClient) {}
    
    placeOrder(order: PlaceOrderDto): Promise<any> {
        return this.http.post<any>(`${environment.apiUrl}/Order`, order).toPromise();
    }

    getOrderDetails(orderId: string) {
        return this.http.get<OrderDto>(`${environment.apiUrl}/Order/`+ orderId);
    }

    getCustomerOrder(status: String): Promise<OrderDto[]> {
        return this.http.get<OrderDto[]>(`${environment.apiUrl}/Client/Orders`).toPromise();
    }

    acceptOffer(acceptOffer: AcceptOfferDto) : Promise<any> {
        return this.http.post<any>(`${environment.apiUrl}/Order/AcceptOffer`, acceptOffer).toPromise();
    }

    rejectOffer(rejectOffer: RejectOfferDto) : Promise<any> {
        return this.http.post<any>(`${environment.apiUrl}/Order/RejectOffer`, rejectOffer).toPromise();
    }
}