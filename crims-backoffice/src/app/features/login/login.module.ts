import { NgModule } from "@angular/core";
import { LoginComponent } from "./presentation/login/login.component";
import { ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "../../app-routing.module";
import { LoginUsecase } from "./domain/usecases/login.usecase";

@NgModule({
    declarations: [
        LoginComponent,
    ],
    imports: [
        ReactiveFormsModule,
    ],
    exports: [
        LoginComponent
    ],
    providers: [
        { provide: LoginUsecase },
    ],
})
export class LoginModule { }