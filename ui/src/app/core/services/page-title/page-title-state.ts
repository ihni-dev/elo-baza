import { Injectable } from '@angular/core';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { PageTitleStateModel } from './page-title-state.model';
import { ChangePageTitle } from './page-title-actions';
import { Title } from '@angular/platform-browser';

const DEFAULT_TITLE: string = 'Elo-Baza';

@State<PageTitleStateModel>({
  name: 'pageTitle',
  defaults: {
    pageTitle: DEFAULT_TITLE,
  },
})
@Injectable()
export class PageTitleState {
  constructor(private bodyTitle: Title) {}

  @Selector()
  static pageTitle(state: PageTitleStateModel) {
    return state.pageTitle;
  }

  @Action(ChangePageTitle)
  changePageTitle(
    ctx: StateContext<PageTitleStateModel>,
    action: ChangePageTitle,
  ) {
    const state = ctx.getState();

    const newPageTitle = action.pageTitle
      ? `${action.pageTitle} | ${DEFAULT_TITLE}`
      : DEFAULT_TITLE;

    this.bodyTitle.setTitle(newPageTitle);

    ctx.setState({
      ...state,
      pageTitle: newPageTitle,
    });
  }
}
