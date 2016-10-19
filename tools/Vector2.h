// Vector2.h 

#pragma once

#include <cmath>

/* 
	This file contains the class for 2D Vectors
*/

typedef Vec2<double> Vector2;
typedef Vec2<float> Vector2F;

const double marginOfError = 0.0001;

// Vec2 class declaration for 2D vector
template<typename T>
class Vec2
{
private:
	T x, y;

public:
	// Constructors
	Vec2();
	Vec2(const Vector2& vec);
	Vec2(const T& x, const T& y);
	
	// Setters and Getters
	void Set(const T& x, const T& y);
	void Set(const Vec2& vec);
	void SetX(const T& x);
	void SetY(const T& y);
	T GetX() const { return x; }
	T GetY() const { return y; }

	// Move the Vector
	void Move(const T& m_x, const T& m_y);
	
	// Overloaded operators
	Vec2& operator+=(const Vec2& vec);
	Vec2& operator-=(const Vec2& vec);
	Vec2& operator*=(const Vec2& vec);
	Vec2& operator/=(const Vec2& vec);
	bool operator==(const Vec2& vec) const;
	bool operator!=(const Vec2& vec) const;
	Vec2& operator=(const Vec2& vec);

	// Formulas
	T Distance(const Vec2& vec) const;
	T Length() const;
	T DotProduct(const Vec2& vec) const;

	Vec2 Normal() const;
};

/*
	Vec2 method definitions
*/

// Constructors
template<typename T>
Vec2<T>::Vec2(): x(0.0), y(0.0)
{ }

template<typename T>
Vec2::Vec2(const Vec2<T>& vec): x(vec.x), y(vec.y)
{ }

template<typename T>
Vec2::Vec2(const T& x_, const T& y_): x(x_), y(y_)
{ }


// Setters
template<typename T>
void Vec2<T>::Set(const T& x_, const T& y_)
{
	x = x_;
	y = y_;
}

template<typename T>
void Vec2<T>::Set(const Vec2<T>& vec)
{
	x = vec.x;
	y = vec.y;
}

template<typename T>
inline void Vec2<T>::SetX(const T& x_)
{
	x = x_;
}

template<typename T>
inline void Vec2<T>::SetY(const T& y_)
{
	y = y_;
}

// Move vector
template<typename T>
void Vec2<T>::Move(const T& m_x, const T& m_y)
{
	x += m_x;
	y += m_y;
}

// Overloaded Operators
template<typename T>
Vec2<T>& Vec2<T>::operator+=(const Vec2<T>& vec)
{
	x += vec.x;
	y += vec.y;

	return *this;
}

template<typename T>
Vec2<T>& Vec2<T>::operator-=(const Vec2<T>& vec)
{
	x -= vec.x;
	y -= vec.y;

	return *this;
}

template<typename T>
Vec2<T>& Vec2<T>::operator*=(const Vec2<T>& vec)
{
	x *= vec.x;
	y *= vec.y;

	return *this;
}

template<typename T>
Vec2<T>& Vec2<T>::operator/=(const Vec2<T>& vec)
{
	x /= vec.x;
	y /= vec.y;

	return *this;
}

// Equality operator
template<typename T>
bool Vec2<T>::operator==(const Vec2<T>& vec) const
{
	return (
		(((vec.x - marginOfError) < x) && (x < (vec.x + marginOfError))) && 
		(((vec.y - marginOfError) < y) && (y < (vec.y + marginOfError))) 
		);
}

// Inequality operator
template<typename T>
bool Vec2<T>::operator!=(const Vec2<T>& vec) const
{
	return (!(*this == vec));
}

// Assignment operator
template<typename T>
Vec2<T>& Vec2<T>::operator=(const Vec2<T>& vec)
{
	// check for self assignment
	if (this != &vec)
		Set(vec);
	return *this;
}

// Distance
template<typename T>
T Vec2<T>::Distance(const Vector2& vec)
{
	return sqrt((vec.x - x) * (vec.x - x) + (vec.y - y) * (vec.y - y));
}

// Length from the origin
template<typename T>
T Vec2<T>::Length()
{
	return sqrt(x * x + y * y);
}

// Dot product
template<typename T>
T Vec2<T>::DotProduct(const Vec2<T>& vec)
{
	return (x * vec.x + y * vec.y);
}

// Normal angle
template<typename T>
Vec2<T> Vec2<T>::Normal()
{
	if (Length())
	{
		const T length = 1/Length();
		return Vec2<T>(x * length, y * length);
	}

	return Vec2<T>(0, 0);
}