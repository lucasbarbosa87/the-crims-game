import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginUsecase } from '../../domain/usecases/login.usecase';
import { LoginDto } from '../../data/dtos/login.dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  focus: boolean = false;
  focus1: boolean = false;

  constructor(private formBuilder: FormBuilder, private loginUsecase: LoginUsecase) {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  get email() {
    return this.loginForm.get("email")!;
  }
  get password() {
    return this.loginForm.get("password")!;
  }

  onSubmit(): void {
    console.log("Inicou o login");
    if (this.loginForm.valid) {
      console.log("é valido");
      this.loginUsecase.execute(new LoginDto(this.email.value, this.password.value));
    }
  }



}
