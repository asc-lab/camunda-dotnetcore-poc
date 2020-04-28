import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SuperPowerService } from '../_services/superpowers-service';
import { OrderService } from '../_services/order-service';
import { PlaceOrderDto } from '../_model/order.dto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-order',
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.css']
})
export class NewOrderComponent implements OnInit {
  form: FormGroup;

  superPowers = [];
  
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private superPowerservice: SuperPowerService,
    private orderService: OrderService) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      superpower: ['', Validators.required],
      dateFrom: ['', [Validators.required]],
      dateTo: ['', Validators.required]
    });

    this.superPowerservice.getAllPowers().subscribe(data => {
      this.superPowers = data;
    });
  }

  async submit() {
    if (this.form.valid) {
      const dto = new PlaceOrderDto(
        this.form.value.superpower,
        this.form.value.dateFrom,
        this.form.value.dateTo
      );
      await this.orderService.placeOrder(dto);
      this.router.navigate(['home']);
    }
  }

}
