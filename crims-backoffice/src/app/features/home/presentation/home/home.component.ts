import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// import { GetUserUsecase } from '../../../../shared/domain/usecases/get-user.usecase';
// import { User } from '../../../../shared/domain/entities/user.entity';
// import { Failure } from '../../../../core/utils/failure';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  constructor(private router: Router,
    // private getUserUsecase: GetUserUsecase
  ) {

  }
  async ngOnInit(): Promise<void> {
    // let user: User = await this.getUserUsecase.execute();
    // if (user == null) {
    //   this.router.navigateByUrl("/login");
    // }
  }
}
