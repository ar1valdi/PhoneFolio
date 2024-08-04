import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewContactFormComponent } from './new-contact-form.component';

describe('NewContactFormComponent', () => {
  let component: NewContactFormComponent;
  let fixture: ComponentFixture<NewContactFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewContactFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewContactFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
