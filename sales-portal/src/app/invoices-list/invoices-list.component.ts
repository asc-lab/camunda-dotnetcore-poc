import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from '../_services/order-service';
import { InvoiceDto } from '../_model/invoice.dto';

@Component({
  selector: 'app-invoices-list',
  templateUrl: './invoices-list.component.html',
  styleUrls: ['./invoices-list.component.css']
})
export class InvoicesListComponent implements OnInit {
  invoices: InvoiceDto[];

  constructor(
    private router: Router,
    private orderService: OrderService
  ) { }

  ngOnInit(): void {
    this.loadInvoices();
  }

  async loadInvoices() {
    this.invoices = await this.orderService.getPendingInvoices();
  }

  async markAsPaid(invoice: InvoiceDto) {
    await this.orderService.markInvoiceAsPaid(invoice.invoiceId);
    this.loadInvoices();
  }
}
