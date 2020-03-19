import { Directive, OnInit, OnDestroy, Input } from '@angular/core';

@Directive({
  selector: '[appSpy]'
})
export class SpyDirective implements OnInit, OnDestroy {

  @Input("appSpy") func: () => {};

  constructor() {}

  ngOnInit() {
    this.func();
  }

  ngOnDestroy() { }

}
