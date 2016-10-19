// Vector3.h 

#pragma once

#include <cmath>

/* 
	This file contains the class for 3D Vectors
*/

typedef Vec3<double> Vector3;
typedef Vec3<float> Vector3F;

const double marginOfError = 0.0001;

// Vec3 class declaration for 3D vector
template<typename T>
class Vec3
{
private:
	T x, y, z;

public:
	// Constructors
	Vec3();
	Vec3(const Vec3& vec);
	Vec3(const T& x, const T& y, const T& z);
	
	// Setters and Getters
	void Set(const T& x, const T& y, const T& z);
	void Set(const Vec3& vec);
	void SetX(const T& x);
	void SetY(const T& y);
	void SetZ(const T& z);
	T GetX() const { return x; }
	T GetY() const { return y; }
	T GetZ() const { return z; }

	// Move the Vector
	void Move(const T& m_x, const T& m_y, const T& m_z);
	
	// Overloaded operators
	Vec3& operator+=(const Vec3& vec);
	Vec3& operator-=(const Vec3& vec);
	Vec3& operator*=(const Vec3& vec);
	Vec3& operator/=(const Vec3& vec);
	bool operator==(const Vec3& vec) const;
	bool operator!=(const Vec3& vec) const;
	Vec3& operator=(const Vec3& vec);

	// Formulas
	T Distance(const Vec3& vec) const;
	T Length() const;
	T DotProduct(const Vec3& vec) const;

	Vec3 CrossProduct(const Vec3& vec) const;
	Vec3 Normal() const;
};

/*
	Vec3 method definitions
*/

// Constructors
template<typename T>
Vec3<T>::Vec3(): x(0.0), y(0.0), z(0.0)
{ }

template<typename T>
Vec3<T>::Vec3(const Vec3<T>& vec): x(vec.x), y(vec.y), z(vec.z)
{ }

template<typename T>
Vec3<T>::Vec3(const T& x_, const T& y_, const T& z_): x(x_), y(y_), z(z_)
{ }

// Setters
template<typename T>
void Vec3<T>::Set(const T& x_, const T& y_, const T& z_)
{
	x = x_;
	y = y_;
	z = z_;
}

template<typename T>
void Vec3<T>::Set(const Vec3<T>& vec)
{
	x = vec.x;
	y = vec.y;
	z = vec.z;
}

template<typename T>
inline void Vec3<T>::SetX(const T& x_)
{
	x = x_;
}

template<typename T>
inline void Vec3<T>::SetY(const T& y_)
{
	y = y_;
}

template<typename T>
inline void Vec3::SetZ(const T& z_)
{
	z = z_;
}

// Move vector
template<typename T>
void Vec3<T>::Move(const T& m_x, const T& m_y, const T& m_z)
{
	x += m_x;
	y += m_y;
	z += m_z;
}

// Overloaded operators
template<typename T>
Vec3<T>& Vec3::operator+=(const Vec3<T>& vec)
{
	x += vec.x;
	y += vec.y;
	z += vec.z;

	return *this;
}

template<typename T>
Vec3<T>& Vec3::operator-=(const Vec3<T>& vec)
{
	x -= vec.x;
	y -= vec.y;
	z -= vec.z;

	return *this;
}

template<typename T>
Vec3<T>& Vec3::operator*=(const Vec3<T>& vec)
{
	x *= vec.x;
	y *= vec.y;
	z *= vec.z;

	return *this;
}

template<typename T>
Vec3<T>& Vec3::operator/=(const Vec3<T>& vec)
{
	x /= vec.x;
	y /= vec.y;
	z /= vec.z;

	return *this;
}

// Equality operator
template<typename T>
bool Vec3<T>::operator==(const Vec3<T>& vec) const
{
	return (
		(((vec.x - marginOfError) < x) && (x < (vec.x + marginOfError))) && 
		(((vec.y - marginOfError) < y) && (y < (vec.y + marginOfError))) &&
		(((vec.z - marginOfError) < z) && (z < (vec.z + marginOfError)))
		);
}

// Inequality operator
template<typename T>
bool Vec3<T>::operator!=(const Vec3<T>& vec) const
{
	return (!(*this == vec));
}

// Assignment operator
template<typename T>
Vec3<T>& Vec3::operator=(const Vec3<T>& vec)
{
	// check for self assignment
	if (this != &vec)
		Set(vec);
	return *this;
}

// Distance
template<typename T>
T Vec3<T>::Distance(const Vec3<T>& vec)
{
	return sqrt((vec.x - x) * (vec.x - x) + (vec.y - y) * (vec.y - y) + (vec.z - z) + (vec.z - z));
}

// Length from the origin
template<typename T>
T Vec3<T>::Length()
{
	return sqrt(x * x + y * y + z * z);
}

// Dot product
template<typename T>
T Vec3<T>::DotProduct(const Vec3<T>& vec)
{
	return (x * vec.x + y * vec.y + z * vec.z);
}

// Cross product
template<typename T>
Vec3<T> Vec3<T>::CrossProduct(const Vec3<T>& vec)
{
	return Vec3<T>(
	(y * vec.z) - (z * vec.y),
	(z * vec.x) - (x * vec.z),
	(x * vec.y) - (y * vec.x) 
	);
}

// Normal angle
template<typename T>
Vec3<T> Vec3<T>::Normal()
{
	if (Length())
	{
		const T length = 1/Length();
		return Vec3<T>(x * length, y * length, z * length);
	}

	return Vec3<T>(0, 0, 0);
}