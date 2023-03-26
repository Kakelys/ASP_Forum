import { AuthInterceptorService } from './auth/auth-interceptor.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NotFoundComponent } from './not-found/not-found.component';
import { HeaderComponent } from './header/header.component';
import { SectionListComponent } from './section-list/section-list.component';
import { SectionComponent } from './section-list/section/section.component';
import { AuthComponent } from './auth/auth.component';
import { FormsModule } from '@angular/forms';
import { LoadingSpinnerComponent } from 'src/shared/loading-spinner/loading-spinner.components';

@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    HeaderComponent,
    SectionListComponent,
    SectionComponent,
    AuthComponent,
    LoadingSpinnerComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
