import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HeroDto } from '../_model/hero.dto';
import { OrderDto } from '../_model/order.dto';
import { TaskDto } from '../_model/task.dto';
import { environment } from '@environments/environment';
import { InvoiceDto } from '../_model/invoice.dto';

@Injectable()
export class OrderService {
    constructor(private http: HttpClient) {
        
    }

    getOrderDetails(orderId: string) {
        return this.http.get<OrderDto>(`${environment.apiUrl}/Order/`+ orderId);
    }

    createOffer(taksId: string, orderId: string, heroId: string) {
        return this.http.post<any>(`${environment.apiUrl}/Order/CreateOffer`, { taskId: taksId, orderId: orderId, selectedHero: heroId});
    }

    rejectOrderWhenNoHeroesAvailable(taksId: string, orderId: string) {
        return this.http.post<any>(`${environment.apiUrl}/Order/NoHeroesAvailable`, { taskId: taksId, orderId: orderId });
    }

    getPendingInvoices() : Promise<InvoiceDto[]> {
        return this.http.get<InvoiceDto[]>(`${environment.apiUrl}/Salesman/PendingInvoices/`).toPromise(); 
    }

    markInvoiceAsPaid(invoiceId: string) {
        return this.http.post<any>(`${environment.apiUrl}/Order/MarkInvoicePaid`, { invoiceId: invoiceId }).toPromise();
    }
}