import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NautreDetailsComponent } from './nautre-details.component';

describe('NautreDetailsComponent', () => {
  let component: NautreDetailsComponent;
  let fixture: ComponentFixture<NautreDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NautreDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NautreDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
