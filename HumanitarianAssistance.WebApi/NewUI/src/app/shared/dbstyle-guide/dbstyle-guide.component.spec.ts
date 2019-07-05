import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DbstyleGuideComponent } from './dbstyle-guide.component';

describe('DbstyleGuideComponent', () => {
  let component: DbstyleGuideComponent;
  let fixture: ComponentFixture<DbstyleGuideComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DbstyleGuideComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DbstyleGuideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
