import {HttpResponseBase} from "@angular/common/http";
import {of} from "rxjs";


export class ClientBase {
  protected transformResult(url: string, response: HttpResponseBase, processor: (response: HttpResponseBase) => any) {
    // TODO: Return own result or throw exception to change default processing behavior,
    // or call processor function to run the default processing logic

    console.log("Service call: " + url);
    processor(response);
    return of(response);
  }
}
