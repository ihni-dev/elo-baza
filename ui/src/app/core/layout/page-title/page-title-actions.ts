export class ChangePageTitle {
  static readonly type = '[PageTitle] Change Page Title';

  constructor(public pageTitle: string) {}
}
