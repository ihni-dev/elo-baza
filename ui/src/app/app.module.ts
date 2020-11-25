import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NavbarModule } from './core/layout/navbar/navbar.module';
import { AppConfigurationService } from './core/app-configuration/app-configuration.service';

import { PageTitleModule } from './core/layout/page-title/page-title.module';
import { NgxsModule } from '@ngxs/store';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsRouterPluginModule } from '@ngxs/router-plugin';

const appInitializerFn = (appConfigurationService: AppConfigurationService) => {
  return () => appConfigurationService.loadConfiguration();
};

@NgModule({
  declarations: [AppComponent],
  imports: [
    NgxsModule.forRoot(),
    NgxsRouterPluginModule.forRoot(),
    NgxsLoggerPluginModule.forRoot(),
    NgxsReduxDevtoolsPluginModule.forRoot(),

    BrowserModule,
    BrowserAnimationsModule,

    NavbarModule,
    PageTitleModule,

    AppRoutingModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializerFn,
      multi: true,
      deps: [AppConfigurationService]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
