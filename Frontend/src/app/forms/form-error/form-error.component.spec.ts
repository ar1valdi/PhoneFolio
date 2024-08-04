import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormTitleComponent } from './form-error.component';

describe('FormTitleComponent', () => {
  let component: FormTitleComponent;
  let fixture: ComponentFixture<FormTitleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormTitleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormTitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
