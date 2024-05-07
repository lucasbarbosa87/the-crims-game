import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { ReactiveFormsModule } from "@angular/forms";
import { CoreModule } from "./core/core.module";
import { SharedModule } from "./shared/shared.module";
import { LoginModule } from "./features/login/login.module";

@NgModule({
    declarations: [
        AppComponent,
    ],
    exports: [
        CoreModule,
        SharedModule,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        ReactiveFormsModule,
        LoginModule,
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }