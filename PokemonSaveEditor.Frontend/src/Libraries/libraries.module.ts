import { NgModule } from "@angular/core";
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from "@angular/common";
import { TrainerCardComponent } from "./trainer-card/trainer-card.component";
import { BadgesCardComponent } from "./badges-card/badges-card.component";
import { UploadFileComponent } from "./upload-file/upload-file/upload-file.component";
import { FormComponent } from "src/app/form/form.component";

@NgModule({
    imports: [
        HttpClientModule,
        CommonModule
    ],
    exports: [
        TrainerCardComponent,
        BadgesCardComponent,
        UploadFileComponent,
        FormComponent
    ],
    declarations: [
        TrainerCardComponent, 
        BadgesCardComponent, 
        UploadFileComponent,
        FormComponent
    ],
    providers: [],
 })
 
 export class LibrariesModule {
 }