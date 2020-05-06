import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { OrderDto, AcceptOfferDto, RejectOfferDto } from '../_model/order.dto';
import { OrderService } from '../_services/order-service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.css']
})
export class OfferDetailsComponent implements OnInit {
  orderId: string;
  taskId: string;
  form: FormGroup;
  order: Observable<OrderDto>;
  
  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
    this.route.paramMap.subscribe(params => {
      this.orderId = params.get('orderId');
      this.taskId = params.get('taskId');
      this.loadOrder();
    });
  }

  initForm(){
    this.form = this.formBuilder.group({
      customerName: [''],
      superpowerName: [''],
      orderFrom: [''],
      orderTo: [''],
      assignedHeroName: ['']
    }); 
  }

  loadOrder(){
    this.order = this.orderService.getOrderDetails(this.orderId)
    .pipe(tap(o => this.form.patchValue(o)));
  }

  backToTasks() {
    this.router.navigate(['home']);
  }

  async accept(){
    await this.orderService.acceptOffer(new AcceptOfferDto(this.taskId, this.orderId));
    this.backToTasks();
  }

  async reject(){
    await this.orderService.rejectOffer(new RejectOfferDto(this.taskId, this.orderId));
    this.backToTasks();
  }

}
