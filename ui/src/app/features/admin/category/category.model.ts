export class Category {
  key: string;
  parentKey: string;
  name: string;
}

export class PagingInfo {
  totalCount: number;
  page: number;
  pageSize: number;
  hasNext: boolean;
  hasPrevious: boolean;
}

export class CategoryResult {
  pagingInfo: PagingInfo;
  data: Category[];
}
