import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth-service';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  returnUrl: string;
  
  constructor(
    private formBuilder: FormBuilder, 
    private auth: AuthService, 
    private router: Router,
    private route: ActivatedRoute) { 
      if (this.auth.currentUserValue) { 
        this.router.navigate(['home']);
    }
    }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required]]
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  submit() {
    if (!this.form.valid) {
      return;
    }

    this.auth.login(this.form.value.username, this.form.value.password)
            .pipe(first())
            .subscribe(
              data => {
                this.router.navigate([this.returnUrl]);
              },
              error => {
                alert(error);
              }
            );
  }
}
