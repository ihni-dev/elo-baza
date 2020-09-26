import { NgModule } from '@angular/core';
import { NgxsModule } from '@ngxs/store';
import { PageTitleState } from './page-title-state';

@NgModule({
  imports: [NgxsModule.forFeature([PageTitleState])]
})
export class PageTitleModule {}
