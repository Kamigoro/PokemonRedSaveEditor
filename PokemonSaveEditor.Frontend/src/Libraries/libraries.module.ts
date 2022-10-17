import { NgModule } from "@angular/core";
import { TrainerCardComponent } from "./trainer-card/trainer-card.component";
import { BadgesCardComponent } from './badges-card/badges-card.component';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from "@angular/common";

@NgModule({
    imports: [
        HttpClientModule,
        CommonModule
    ],
    exports: [
        TrainerCardComponent,
        BadgesCardComponent
    ],
    declarations: [
        TrainerCardComponent, 
        BadgesCardComponent],
    providers: [],
 })
 
 export class LibrariesModule {
 }