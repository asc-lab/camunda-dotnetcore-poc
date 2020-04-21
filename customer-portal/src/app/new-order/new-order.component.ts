import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-new-order',
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.css']
})
export class NewOrderComponent implements OnInit {
  form: FormGroup;

  superPowers = [
    {code: 'NIGHT_VISION', name: 'Night Vision'},
    {code: 'LIGHT_SPEED', name: 'Light Speed'},
    {code: 'LASER_EYES', name: 'Laser eyes'},
    {code: 'SUPER_STRENGTH' , name: 'Super Strength'}
  ];
  
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      superpower: ['', Validators.required],
      dateFrom: ['', [Validators.required]],
      dateTo: ['', Validators.required]
    });
  }

  submit() {
    if (this.form.valid) {
      console.log(this.form.value);
    }
  }

}
