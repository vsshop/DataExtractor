import { StatusType } from "../enums/status.type";

export interface BaseResponse<T> {
  data: T;
  message: string;
  status: StatusType;
}
