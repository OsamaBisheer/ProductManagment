import { ResponseCodeEnum } from '../enums/ResponseCodeEnum';

export class ResponseModel {
  code?: ResponseCodeEnum;
  messageFL?: string;
  messageSL?: string;
  result?: any;
}
