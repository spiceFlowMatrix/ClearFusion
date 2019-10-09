import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditGeneratorComponent } from './edit-generator.component';

describe('EditGeneratorComponent', () => {
  let component: EditGeneratorComponent;
  let fixture: ComponentFixture<EditGeneratorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditGeneratorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditGeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
