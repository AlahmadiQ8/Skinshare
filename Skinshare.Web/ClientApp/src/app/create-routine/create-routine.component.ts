import {Component, OnDestroy, OnInit, Inject} from '@angular/core';
import {IStepRequest, PartOfDay, RoutineRequest, RoutinesClient, RoutineResponse} from "../app.generated";
import {FormArray, FormBuilder, Validators} from "@angular/forms";
import { WINDOW_TOKEN } from '../di-tokens';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-create-routine',
  templateUrl: './create-routine.component.html',
  styleUrls: ['./create-routine.component.css']
})
export class CreateRoutineComponent implements OnInit, OnDestroy {
  public routine;
  public routineForm = this.fb.group({
    title: ['', Validators.compose([Validators.required, Validators.minLength(5)])],
    description: [''],
    morningSteps: this.fb.array([
      ['', Validators.required]
    ]),
    eveningSteps: this.fb.array([
      ['', Validators.required]
    ]),
  });
  subscription: Subscription;

  constructor(private routinesClient: RoutinesClient, private fb: FormBuilder, @Inject(WINDOW_TOKEN) private window: Window) {
  }

  get title() { return this.routineForm.get('title') }
  get morningSteps() { return this.routineForm.get('morningSteps') as FormArray; }
  get eveningSteps() { return this.routineForm.get('eveningSteps') as FormArray; }

  ngOnInit() {

  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onSubmit(): void {
    const body = this.routineDto(this.routineForm.value);
    this.subscription = this.routinesClient.postRoutine(body).subscribe(response => {
      const routine = response as RoutineResponse;
      this.window.location.href = routine.href;
    });
  }

  addStep(formName: string) {
    (this[formName] as FormArray).push(this.fb.control('', Validators.required));
  }

  private routineDto(form: RoutineFormResult): RoutineRequest {
    return RoutineRequest.fromJS({
      title: form.title,
      description: form.description,
      steps: [...this.stepDto(form.morningSteps, PartOfDay.Morning), ...this.stepDto(form.eveningSteps, PartOfDay.Evening)]
    });
  }

  private stepDto(steps: string[], partOfDay: PartOfDay): IStepRequest[] {
    return steps.map<IStepRequest>((step, index) => ({
      description: step,
      order: index,
      partOfDay: partOfDay
    }));
  }
}

interface RoutineFormResult {
  title: string,
  description: string,
  morningSteps: string[],
  eveningSteps: string[]
}
