import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ThrowStmt } from '@angular/compiler';
import { OrderService } from '../_services/order-service';
import { Observable } from 'rxjs';
import { OrderDto } from '../_model/order.dto';
import { tap, first } from 'rxjs/operators';
import { HeroService } from '../_services/hero-service';

@Component({
  selector: 'app-create-offer',
  templateUrl: './create-offer.component.html',
  styleUrls: ['./create-offer.component.css']
})
export class CreateOfferComponent implements OnInit {
  orderId: string;
  taskId: string;
  form: FormGroup;
  order: Observable<OrderDto>;
  availableHeroes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private orderService: OrderService,
    private heroService: HeroService
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.route.paramMap.subscribe(params => {
      this.orderId = params.get('orderId');
      this.taskId = params.get('taskId');
      this.loadOrder();
      this.findHeroes();
    });
  }

  async findHeroes() {
    this.availableHeroes = await this.heroService.findAvailableHeroes(this.orderId);
  }

  initForm() {
    this.form = this.formBuilder.group({
      customerName: [''],
      superpowerName: [''],
      orderFrom: [''],
      orderTo: [''],
      assignedHeroId: ['', Validators.required]
    });  
  }

  loadOrder() {
    this.order = this.orderService.getOrderDetails(this.orderId)
    .pipe(tap(o => this.form.patchValue(o)));
  }

  backToTasks() {
    this.router.navigate(['home']);
  }

  async noHeroes() {
    await this.orderService.rejectOrderWhenNoHeroesAvailable(this.taskId, this.orderId).toPromise();
    this.router.navigate(['home']);
  }

  submit(){
    if (!this.form.valid) {
      return;
    }

    this.orderService
      .createOffer(this.taskId, this.orderId, this.form.value.assignedHeroId)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['home']);
        },
        error => {
          alert(error);
        }
      );
  }

}
