import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { LibrariesModule } from '../libraries/libraries.module';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    LibrariesModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
