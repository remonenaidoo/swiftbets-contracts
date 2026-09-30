// Generated from SwiftBets.Contracts. Do not edit; run `dotnet run --project tools/SwiftBets.Contracts.SchemaGen`.

export interface CouponPlacedV1 {
    couponId: string;
    punterId: string;
    betType: BetType;
    stake: Money;
    totalOdds: number;
    potentialPayout: Money;
    legs: CouponLegV1[];
    placedAt: string;
}

export type BetType = "single" | "accumulator";

/** An amount in minor units (cents) of an ISO 4217 currency. */
export interface Money {
    minorUnits: number;
    currency: string;
}

export interface CouponLegV1 {
    legId: string;
    fixtureId: string;
    marketId: string;
    selectionId: string;
    odds: number;
    offerVersion: number;
}

export interface ResultPublishedV1 {
    fixtureId: string;
    resultVersion: number;
    status: ResultStatus;
    homeGoals: number;
    awayGoals: number;
    publishedAt: string;
}

/** Ordered by precedence: a higher value outranks a lower one at the same result version. */
export type ResultStatus = "provisional" | "official" | "correction" | "void";

export interface CouponRejectedV1 {
    couponId: string;
    punterId: string;
    reasonCode: string;
    rejectedAt: string;
}

/** Full snapshot of a fixture's offer; OfferVersion increases on every price or status change. */
export interface FixtureChangedV1 {
    fixtureId: string;
    competition: string;
    homeTeam: string;
    awayTeam: string;
    kickoffAt: string;
    status: FixtureStatus;
    offerVersion: number;
    markets: MarketV1[];
    changedAt: string;
}

export type FixtureStatus = "scheduled" | "inPlay" | "finished" | "postponed";

export interface MarketV1 {
    marketId: string;
    type: MarketType;
    status: MarketStatus;
    selections: SelectionV1[];
}

export type MarketType = "matchResult" | "totalGoalsOverUnder25";

export type MarketStatus = "open" | "suspended" | "closed";

export interface SelectionV1 {
    selectionId: string;
    name: string;
    odds: number;
}

/** RFC 7807 problem details plus a stable machine code and the request's correlation id. */
export interface ErrorEnvelope {
    type: string;
    title: string;
    status: number;
    code: string;
    correlationId: string;
    detail: string | undefined;
    instance: string | undefined;
    errors: FieldError[] | undefined;
}

export interface FieldError {
    field: string;
    code: string;
    message: string;
}
