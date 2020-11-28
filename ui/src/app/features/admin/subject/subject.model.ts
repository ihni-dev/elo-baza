export class Subject {
  key: string;
  name: string;
}

export class PagingInfo {
  totalCount: number;
  page: number;
  pageSize: number;
  hasNext: boolean;
  hasPrevious: boolean;
}

export class SubjectResult {
  pagingInfo: PagingInfo;
  data: Subject[];
}
