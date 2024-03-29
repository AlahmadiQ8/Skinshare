import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { APP_BASE_HREF, Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { RoutinesClient } from "./app.generated";
import { CreateRoutineComponent } from './create-routine/create-routine.component';
import { WINDOW_TOKEN } from './di-tokens';
import { SpyDirective } from './directives/spy.directive';

@NgModule({
  declarations: [
    AppComponent,
    CreateRoutineComponent,
    SpyDirective
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    RoutinesClient, Location,
    { provide: LocationStrategy,useClass: PathLocationStrategy },
    { provide: APP_BASE_HREF, useValue: '/' },
    { provide: WINDOW_TOKEN, useValue: window}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
